﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace TestXamarinFirebase
{
	public class CustomCircle : Map
	{
		public Position Position { get; set; }

		public double Radius { get; set; }
	}
}
