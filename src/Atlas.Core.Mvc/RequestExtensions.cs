// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestExtensions.cs" company="Epworth Consulting Ltd.">
//   © Epworth Consulting Ltd.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Atlas.Core.Mvc
{
   using System;
   using System.Linq;
   using System.Net;
   using System.Web;

   public static class RequestExtensions
   {
      private const string UnknownIp = "0.0.0.0";

      public static string GetCallerIdentity(this HttpRequestBase request)
      {
         var userHostAddress = request.UserHostAddress;

         if (string.IsNullOrEmpty(userHostAddress))
         {
            return UnknownIp;
         }

         IPAddress ipAddress;

         if (!IPAddress.TryParse(userHostAddress, out ipAddress))
         {
            return UnknownIp;
         }

         var forwardedFor = request.ServerVariables["X_FORWARDED_FOR"];

         if (string.IsNullOrEmpty(forwardedFor))
         {
            return userHostAddress;
         }

         try
         {
            // Get a list of public ip addresses in the X_FORWARDED_FOR variable
            var publicForwardingIps = forwardedFor.Split(',').Where(ip => !IsIpAddressPrivate(ip)).ToList();

            // If we found any, return the last one, otherwise return the user host address
            return publicForwardingIps.Any() ? publicForwardingIps.Last() : userHostAddress;
         }
         catch (Exception)
         {
            return UnknownIp;
         }
      }

      /// <summary>
      ///   http://en.wikipedia.org/wiki/Private_network
      ///   Private IP Addresses are: 
      ///     24-bit block: 10.0.0.0 through 10.255.255.255
      ///     20-bit block: 172.16.0.0 through 172.31.255.255
      ///     16-bit block: 192.168.0.0 through 192.168.255.255
      ///   Link-local addresses:
      ///     169.254.0.0 through 169.254.255.255 (http://en.wikipedia.org/wiki/Link-local_address)
      /// </summary>
      /// <returns>
      /// True if IP address is private
      /// </returns>
      private static bool IsIpAddressPrivate(string ipAddress)
      {
         var ip = IPAddress.Parse(ipAddress);
         var octets = ip.GetAddressBytes();

         var is24BitBlock = octets[0] == 10;
         if (is24BitBlock)
         {
            return true;
         }

         var is20BitBlock = octets[0] == 172 && octets[1] >= 16 && octets[1] <= 31;
         if (is20BitBlock)
         {
            return true;
         }

         var is16BitBlock = octets[0] == 192 && octets[1] == 168;
         if (is16BitBlock)
         {
            return true;
         }

         var isLinkLocalAddress = octets[0] == 169 && octets[1] == 254;
         return isLinkLocalAddress;
      }
   }
}
