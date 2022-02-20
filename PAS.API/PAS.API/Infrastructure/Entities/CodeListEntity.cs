using PAS.API.Constants;
using PAS.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PAS.API.Infrastructure.Entities
{
    /// <summary>
    /// CodeList Entity class
    /// </summary>
    [Table("code_list", Schema = DbConstant.PolicyAdministrationSystemSchema)]
    public class CodeListEntity
    {
        /// <summary>
        /// CodeListId for CodeList
        /// </summary>
        [Key]
        [Column("code_list_id")]
        public Guid CodeListId { get; set; }

        /// <summary>
        /// CodeList title
        /// </summary>
        [Column("code_list_title")]
        public string CodeListTitle { get; set; }

        /// <summary>
        /// CodeList reference
        /// </summary>
        [Column("code_list_reference")]
        public string CodeListReference { get; set; }

        /// <summary>
        /// CodeList description
        /// </summary>
        [Column("code_list_description")]
        public string CodeListDescription { get; set; }

        /// <summary>
        /// Enumeration CodeList in JSON
        /// </summary>
        [Column("enumeration_code_list")]
        public IEnumerable<EnumerationCode> EnumerationCodeList { get; set; }
    }
}
