// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

namespace SportsPro.Models
{
    public class Check
    {
        public static string EmailExists(IRepository<Customer> ctx,
        string email)
        {
            var options = new QueryOptions<Customer>
            {
                Where = c => c.Email.ToLower() == email.ToLower()
            };
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.Get(options);
                if (customer != null)
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }

    }
}
