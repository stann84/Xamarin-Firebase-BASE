﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CustomMapPage : ContentPage
{
    public CustomMapPage()
		{
			InitializeComponent();

			var pin = new Pin
			{
				Type = PinType.Place,
				Position = new Position(37.79752, -122.40183),
				Label = "Xamarin San Francisco Office",
				Address = "394 Pacific Ave, San Francisco CA"
			};

			var position = new Position(37.79752, -122.40183);
			customMap.Circle = new CustomCircle
			{
				Position = position,
				Radius = 1000
			};

			customMap.Pins.Add(pin);
			customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.0)));
		}
	}
}