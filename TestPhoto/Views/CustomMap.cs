using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace TestXamarinFirebase
{
	public class CustomMap : Map
	{
		public CustomCircle Circle { get; set; }
        public List<CustomPin> CustomPins { get; set; }
		public List<CustomCircle> CustomCircles { get; set; }


		public CustomMap()
		{
			
			CustomPins = new List<CustomPin>();
			CustomCircles = new List<CustomCircle>();
		}
	}
}
