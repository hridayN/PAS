using ExcelDataReader;
using PAS.API.Constants;
using PAS.API.DTO;
using PAS.API.Infrastructure.Contracts;
using PAS.API.Infrastructure.Entities;
using PAS.API.Mapper;
using PAS.API.Models;
using PAS.API.Services.Contract;
using PAS.API.Utilites;
using System.Data;

namespace PAS.API.Services.Core
{
    /// <summary>
    /// CodeList Service
    /// </summary>
    public class CodeListService : ICodeListService
    {
        /// <summary>
        /// Codelist repository variable
        /// </summary>
        private readonly IRepository<CodeListEntity> _codeListRepository;

        public CodeListService(IRepository<CodeListEntity> codeListRepository)
        {
            _codeListRepository = codeListRepository;
        }

        /// <summary>
        /// Imports Code List From Excel File
        /// </summary>
        /// <param name="file">Form File</param>
        /// <param name="importCodeListRequest">importCodeListRequest</param>
        /// <returns></returns>
        public async Task<ImportCodeListResponse> ImportCodeList(IFormFile file, ImportCodeListRequest importCodeListRequest)
        {
            ImportCodeListResponse importCodeListResponse = new ImportCodeListResponse();
            List<CodeList> codeLists = new List<CodeList>();
            if (file == null)
            {
                Utility.AddErrorMessage(importCodeListResponse.MessageInformation, Errors.CodeListFileMissing);
                Utility.SetStatus(400, Messages.CodeListFileMissing, importCodeListResponse.MessageInformation);
                return importCodeListResponse;
            }

            if (Path.GetExtension(file.FileName) != ".xlsx")
            {
                Utility.AddErrorMessage(importCodeListResponse.MessageInformation, Errors.InvalidExcelFile);
                Utility.SetStatus(400, Messages.InvalidExcelFile, importCodeListResponse.MessageInformation);
                return importCodeListResponse;
            }

            if (ValidateSheetColumn(file, importCodeListResponse.MessageInformation))
            {
                FetchDataFromFile(file, codeLists);
                importCodeListResponse.ImportStatus = new List<ImportCodeList>();
                foreach (CodeList code in codeLists)
                {
                    bool isSkipped = await SaveCodeList(code);
                    if (!isSkipped)
                    {
                        importCodeListResponse.ImportStatus.Add(new ImportCodeList()
                        {
                            CodeListReference = code.CodeListReference,
                            CodeListVersion = code.CodeListVersion,
                            Status = "Imported",
                            Records = code.EnumerationCodeList.Count()
                        });
                    }
                    else
                    {
                        importCodeListResponse.ImportStatus.Add(new ImportCodeList()
                        {
                            CodeListReference = code.CodeListReference,
                            Status = "Skipped",
                            Records = code.EnumerationCodeList.Count()
                        });
                    }

                }
                Utility.SetOkStatus(Success.CodelistImported, importCodeListResponse.MessageInformation);
            }
            else
            {
                Utility.SetStatus(400, Messages.ExcelIsNotValid, importCodeListResponse.MessageInformation);
            }
            return importCodeListResponse;
        }

        /// <summary>
        /// Save reference data in DB
        /// </summary>
        /// <param name="loadCodeListRequest"></param>
        /// <returns></returns>
        public async Task<LoadCodeListResponse> LoadCodeList(LoadCodeListRequest loadCodeListRequest)
        {
            LoadCodeListResponse loadCodeListResponse = new LoadCodeListResponse();
            bool isSaved = await SaveCodeList(loadCodeListRequest.CodeList);
            if (isSaved)
            {
                Utility.AddErrorMessage(loadCodeListResponse.MessageInformation, Errors.InvalidExcelFile);
                Utility.SetStatus(400, Messages.InvalidExcelFile, loadCodeListResponse.MessageInformation);
            }
            else
            {
                Utility.SetOkStatus(Success.DataSaved, loadCodeListResponse.MessageInformation);
            }
            return loadCodeListResponse;
        }

        private async Task<bool> SaveCodeList(CodeList codeList)
        {
            bool savedRecord = false;
            var dbLists = await _codeListRepository.GetOneAsyncWithOrder(x => x.CodeListReference.ToLower() == codeList.CodeListReference.ToLower(), "CodeListVersion", false);
            if (dbLists != null && dbLists.CodeListDescription == codeList.CodeListDescription && dbLists.CodeListTitle == codeList.CodeListTitle)
            {
                savedRecord = codeList.EnumerationCodeList.All(x => 
                dbLists.EnumerationCodeList.Any(y => x.Description == y.Description && x.DisplayValue == y.DisplayValue && x.CodeValue == y.CodeValue && 
                (x.SubCodeValue?.All(z => y.SubCodeValue?.Any(m => m == z) ?? true) ?? true))) &&
                     dbLists.EnumerationCodeList.All(x => codeList.EnumerationCodeList.Any(y => x.Description == y.Description && x.DisplayValue == y.DisplayValue && x.CodeValue == y.CodeValue && (x.SubCodeValue?.All(z => y.SubCodeValue?.Any(m => m == z) ?? true) ?? true)));
            }
            if (!savedRecord)
            {
                codeList.CodeListVersion = dbLists?.CodeListVersion == null ? codeList.CodeListVersion : dbLists.CodeListVersion + 1;
                var codeListEntity = ObjectMapper.Mapper.Map<CodeListEntity>(codeList);
                await _codeListRepository.AddAsync(codeListEntity);
            }
            return savedRecord;
        }

        /// <summary>
        /// Validates the uploaded sheet columns
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="messageInformation">Message Information Object</param>
        /// <returns></returns>
        private bool ValidateSheetColumn(IFormFile file, MessageInformation messageInformation)
        {
            bool isValid = true;
            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    DataSet result = reader.AsDataSet(conf);
                    foreach (DataTable table in result.Tables)
                    {
                        if (table.TableName != "Code_Details")
                        {
                            if (!table.Columns.Contains("Code"))
                            {
                                isValid = false;

                            }
                            if (!table.Columns.Contains("Description"))
                            {
                                isValid = false;
                                Utility.AddErrorMessage(messageInformation, Errors.ExcelDataMissing, string.Format(Errors.ExcelDataMissing.ErrorDescription, table.TableName, "Description"));
                            }
                        }

                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Fetches the data from sheet and filles in object
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="lists">List where fethced data will be store</param>
        private void FetchDataFromFile(IFormFile file, List<CodeList> lists)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    DataSet result = reader.AsDataSet(conf);
                    DataTable sheetReference = result.Tables["Code_Details"];
                    foreach (DataTable table in result.Tables)
                    {
                        if (table.TableName != "Code_Details")
                        {
                            ValidateAndAddData(table, lists, sheetReference);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates Data And add in list for saving later
        /// </summary>
        /// <param name="table">Actual Data</param>
        /// <param name="lists">List where need to add if valid</param>
        /// <param name="sheetReference">reference sheet where will get some details</param>
        private void ValidateAndAddData(DataTable table, List<CodeList> lists, DataTable sheetReference)
        {
            string codeListDescription = table.TableName;
            string codeTitle = table.TableName;
            string codeSource = "Internal";
            var DetailsSheet = sheetReference?.AsEnumerable()?.Where(x => x.Field<string>("Reference").ToString() == table.TableName);
            if (DetailsSheet?.Any() ?? false)
            {
                codeListDescription = sheetReference.Columns.Contains("Description") ? DetailsSheet.First()["Description"].ToString() : codeListDescription;
                codeTitle = sheetReference.Columns.Contains("Title") ? DetailsSheet.First()["Title"].ToString() : codeTitle;
                codeSource = sheetReference.Columns.Contains("Source") ? DetailsSheet.First()["Source"].ToString() : codeSource;
            }
            CodeList codeList = new CodeList()
            {
                CodeListDescription = codeListDescription.Trim(),
                CodeListReference = table.TableName.Trim(),
                CodeListTitle = string.IsNullOrEmpty(codeTitle) ? codeListDescription.Trim() : codeTitle.Trim(),
                CodeListVersion = 1,
                EnumerationCodeList = new List<EnumerationCode>()
            };

            foreach (DataRow row in table.Rows)
            {
                codeList.EnumerationCodeList = codeList.EnumerationCodeList.Append(new EnumerationCode()
                {
                    CodeValue = row["Code"].ToString().Trim(),
                    Description = row["Description"].ToString().Trim(),
                    DisplayValue = row["Description"].ToString().Trim(),
                    SubCodeValue = row["SubCode"].ToString().Trim()
                });
            }
            if (codeList.EnumerationCodeList.Any())
            {
                lists.Add(codeList);
            }
        }
    }
}
