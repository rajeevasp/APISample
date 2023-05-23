using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Infrastructure
{
    /// <summary>
    /// Creates KML objects from a variety of input sources
    /// </summary>
    public static class KMLHelper
    {
        private static readonly string ICAOMarkerIconUrl = "https://www.google.co.in/";

        /// <summary>
        /// Create the alternate airport KML object.
        /// </summary>
        /// <param name="airports">IList of alternative <see cref="Domain.Airport.Airport"/></param>
        /// <returns></returns>
        //internal static Kml CreateAlternateAirportKml(IList<Domain.Airport.Airport> airports)
        //{
        //    var kml = new Kml();
        //    var document = new Document();

        //    //foreach (var airport in airports)
        //    //{
        //    //    var placemark = new Placemark();
        //    //    placemark.Id = airport.ICAO;
        //    //    placemark.Geometry = CreatePoint(airport.Latitude, airport.Longitude);
        //    //    placemark.AddStyle(CreateStyle(airport.ICAO));
        //    //    document.AddFeature(placemark);
        //    //}

        //    kml.Feature = document;
        //    return kml;
        //}

        #region Helpers

        /// <summary>
        /// Creates data for the extendeddata node
        /// </summary>
        /// <param name="dataName">Name of data node</param>
        /// <param name="value">Value of data</param>
        /// <returns><see cref="SharpKml.Dom.Data"/></returns>
        private static SharpKml.Dom.Data CreateData(string dataName, string value)
        {
            return new SharpKml.Dom.Data {
                Name = dataName,
                Value = value
            };
        }

        /// <summary>
        /// Creates a style object for a placemark
        /// </summary>
        /// <param name="icao"></param>
        /// <returns></returns>
        private static Style CreateStyle(string icao)
        {
            IconStyle iconStyle = new IconStyle();
            iconStyle.Icon = new IconStyle.IconLink(
                new Uri(string.Format("{0}?code={1}&zoom=100", ICAOMarkerIconUrl, icao)
            ));
            iconStyle.Scale = 1;

            return new Style {
                Icon = iconStyle
            };
        }

        /// <summary>
        /// Creates a point object and sets the coordinates
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns><see cref="Point"/></returns>
        private static Point CreatePoint(string lat, string lon)
        {
            double latitude;
            double longitude;

            if (!double.TryParse(lat, out latitude)) 
            {
                latitude = 0;
            }

            if (!double.TryParse(lon, out longitude))
            {
                longitude = 0;
            }

            return new Point {
                Coordinate = new Vector(latitude, longitude)
            };
        }

        #endregion
    }
}