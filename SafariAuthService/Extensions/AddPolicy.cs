namespace Ecommerce.Extensions
{
    public static  class AddPolicy
    {

        public static WebApplicationBuilder AddminPolicy (this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("adminPolicy", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.RequireClaim("Role","Admin");
                });
            });

            return builder;
        }

    }
}
