// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CookieHelper.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Linq;
   using System.Web;
   using System.Web.Security;

   public class CookieHelper : ICookieHelper
   {
      public const string CookieConsentCookie = "CookieConsent";

      private readonly IHttpHelper httpHelper;

      public CookieHelper(IHttpHelper httpHelper)
      {
         if (httpHelper == null)
         {
            throw new ArgumentNullException("httpHelper");
         }

         this.httpHelper = httpHelper;
      }

      public bool? ConsentGiven
      {
         get
         {
            var cookieConsent = this.GetCookie(CookieConsentCookie);

            if (cookieConsent == null || string.IsNullOrEmpty(cookieConsent.Value))
            {
               return null;
            }

            return bool.Parse(cookieConsent.Value);
         }

         set
         {
            this.Set(CookieConsentCookie, value);
         }
      }

      public bool HasConsent
      {
         get { return this.ConsentGiven ?? true; }
      }

      public T Get<T>(string name, Func<string, T> converterFunc, T defaultValue)
      {
         var cookie = this.GetCookie(name);

         if (cookie == null || string.IsNullOrEmpty(cookie.Value))
         {
            return defaultValue;
         }

         return converterFunc(cookie.Value);
      }

      public string Get(string name)
      {
         var cookie = this.GetCookie(name);

         if (cookie == null || string.IsNullOrEmpty(cookie.Value))
         {
            return null;
         }

         return cookie.Value;
      }

      public T? GetNullable<T>(string name, Func<string, T> converterFunc)
         where T : struct
      {
         var cookie = this.GetCookie(name);

         if (cookie == null || cookie.Value == string.Empty)
         {
            return null;
         }

         return converterFunc(cookie.Value);
      }

      public void Set<T>(string name, T value)
      {
         if (name != CookieConsentCookie && name != FormsAuthentication.FormsCookieName && !this.HasConsent)
         {
            return;
         }

         var cookie = this.GetCookie(name);

         if (cookie == null)
         {
            // Create the cookie if it doesn't already exist
            cookie = new HttpCookie(name) { HttpOnly = true, Expires = DateTime.Today.AddMonths(3) };
         }

         // Set the cookie value
         cookie.Value = value.ToString();

         // Add the cookie to the response
         this.httpHelper.CurrentResponse.Cookies.Add(cookie);
      }

      public void Remove(string name)
      {
         var cookie = this.GetCookie(name);

         if (cookie == null)
         {
            return;
         }

         this.httpHelper.CurrentResponse.Cookies.Remove(name);

         var empty = string.Empty;

         if (!this.httpHelper.CurrentRequest.Browser.SupportsEmptyStringInCookieValue)
         {
            empty = "NoCookie";
         }

         cookie = new HttpCookie(name, empty) { HttpOnly = true, Expires = DateTime.Today.AddDays(-1) };

         this.httpHelper.CurrentResponse.Cookies.Add(cookie);
      }

      private HttpCookie GetCookie(string name)
      {
         if (name != CookieConsentCookie && name != FormsAuthentication.FormsCookieName && !this.HasConsent)
         {
            return null;
         }

         if (this.httpHelper.CurrentResponse.Cookies.AllKeys.Contains(name))
         {
            return this.httpHelper.CurrentResponse.Cookies[name];
         }

         if (this.httpHelper.CurrentRequest.Cookies.AllKeys.Contains(name))
         {
            return this.httpHelper.CurrentRequest.Cookies[name];
         }

         return null;
      }
   }
}
