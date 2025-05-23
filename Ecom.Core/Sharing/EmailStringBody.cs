using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Sharing
{
    public class EmailStringBody
    {
        public static string Send(string email, string token, string component, string message)
        {
            string encodeToken = Uri.EscapeDataString(token);

            return $@"
        <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                        padding: 20px;
                    }}
                    .container {{
                        background-color: #ffffff;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                        max-width: 600px;
                        margin: 0 auto;
                    }}
                    h1 {{
                        color: #2c3e50;
                    }}
                    a.button {{
                        display: inline-block;
                        margin-top: 20px;
                        padding: 10px 20px;
                        background-color: #007BFF;
                        color: #ffffff;
                        text-decoration: none;
                        border-radius: 5px;
                        font-weight: bold;
                    }}
                    a.button:hover {{
                        background-color: #0056b3;
                    }}
                    hr {{
                        border: none;
                        border-top: 1px solid #ddd;
                        margin: 20px 0;
                    }}
                </style>
            </head>
            <body>
                <div class=""container"">
                    <h1>{message}</h1>
                    <hr />
                    <p>To proceed, please click the button below:</p>
                    <a class=""button"" href=""http://localhost:4200/Account/{component}?email={email}&code={encodeToken}"">Continue</a>
                </div>
            </body>
        </html>";
        }

    }
}
