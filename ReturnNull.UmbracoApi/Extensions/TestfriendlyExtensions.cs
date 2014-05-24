using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using umbraco;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace ReturnNull.UmbracoApi
{
    /// <summary>
    /// Provides extension methods that can replace the use of existing Umbraco
    /// methods which have hard dependencies on non-mockable objects.
    /// These methods must remain unit testable with no dependencies on complex
    /// items such as file, session, database etc.
    /// </summary>
    public static class TestfriendlyExtensions
    {

        /// <summary>
        /// Returns the original property value via the Umbraco API
        /// If the property is not found, returns null.
        /// </summary>
        public static object GetRawValueOfProperty(this IPublishedContent content, string alias)
        {
            var property = content.GetProperty(alias);
            return property == null ? null : property.Value;
        }

        /// <summary>
        /// Returns the property value via the Umbraco API converted to the desired type.
        /// If property is not found or if the value cannot be cast, a default value will 
        /// be returned.
        /// </summary>
        public static TReturn GetValueOfProperty<TReturn>(this IPublishedContent content, string alias,
            TReturn defaultValue)
        {
            var property = content.GetProperty(alias);
            if (property == null)
                return defaultValue;

            var value = property.Value;
            if (value is TReturn)
                return (TReturn) property.Value;

            try
            {
                return (TReturn) Convert.ChangeType(value, typeof (TReturn));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns the property value via the Umbraco API converted to the desired type.
        /// This method throw an UmbracoException if there are any problems finding or 
        /// converting the property value.
        /// </summary>
        public static TReturn GetValueOfProperty<TReturn>(this IPublishedContent content, string alias)
        {
            try
            {
                var property = content.GetProperty(alias);
                var value = property.Value;
                if (value is TReturn)
                    return (TReturn) value;
                return (TReturn) Convert.ChangeType(value, typeof (TReturn));
            }
            catch (Exception exception)
            {
                throw new UmbracoException(
                    "Unable to find or convert property value to desired type. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Returns the property value as a string
        /// </summary>
        public static string GetValueOfProperty(this IPublishedContent content, string alias)
        {
            return content.GetValueOfProperty<string>(alias);
        }
    }
}