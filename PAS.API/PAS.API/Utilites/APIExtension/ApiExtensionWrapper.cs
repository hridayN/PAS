namespace PAS.API.Utilites.APIExtension
{
    /// <summary>
    /// Api Extension class for common API methods
    /// </summary>
    public static class ApiExtensionWrapper
    {
        /// <summary>
        /// Common method for setup basic settings for API
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection SetUpApi(this IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllers();
            return services;
        }

        public static void ConfigureApi(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
