﻿using System;
using System.Linq;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps;

namespace Microsoft.Maui.Controls.Compatibility.ControlGallery.GalleryPages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapElementsGallery : ContentPage
	{
		enum SelectedElementType
		{
			Polyline,
			Polygon,
			Circle
		}

		SelectedElementType _selectedType;

		Map.Polyline _polyline;
		Map.Polygon _polygon;
		Map.Circle _circle;

		Random _random = new Random();

		public MapElementsGallery()
		{
			InitializeComponent();

			Map.MoveToRegion(
				MapSpan.FromCenterAndRadius(
					new Devices.Sensors.Location(39.828152, -98.569817),
					Distance.FromMiles(1681)));

			_polyline = new Maps.Polyline
			{
				Geopath =
				{
					new Devices.Sensors.Location(47.641944, -122.127222),
					new Devices.Sensors.Location(37.411625, -122.071327),
					new Devices.Sensors.Location(35.138901, -80.922623)
				}
			};

			_polygon = new Maps.Polygon
			{
				StrokeColor = Color.FromArgb("#002868"),
				FillColor = Color.FromArgb("#88BF0A30"),
				Geopath =
				{
					new Devices.Sensors.Location(37, -102.05),
					new Devices.Sensors.Location(37, -109.05),
					new Devices.Sensors.Location(41, -109.05),
					new Devices.Sensors.Location(41, -102.05)
				}
			};

			_circle = new Circle
			{
				Center = new Devices.Sensors.Location(42.352364, -71.067177),
				Radius = Distance.FromMiles(100.0),
				StrokeColor = Color.FromRgb(31, 174, 206),
				FillColor = Color.FromRgba(31, 174, 206, 127)
			};

			Map.MapElements.Add(_polyline);
			Map.MapElements.Add(_polygon);
			Map.MapElements.Add(_circle);

			ElementPicker.SelectedIndex = 0;
		}

		void MapClicked(object sender, MapClickedEventArgs e)
		{
			switch (_selectedType)
			{
				case SelectedElementType.Polyline:
					_polyline.Geopath.Add(e.Location);
					break;
				case SelectedElementType.Polygon:
					_polygon.Geopath.Add(e.Location);
					break;
				case SelectedElementType.Circle:
					if (_circle.Center == default(Devices.Sensors.Location))
					{
						_circle.Center = e.Location;
					}
					else
					{
						_circle.Radius = Distance.BetweenPositions(_circle.Center, e.Location);
					}
					break;
			}
		}

		void PickerSelectionChanged(object sender, EventArgs e)
		{
			Enum.TryParse((string)ElementPicker.SelectedItem, out _selectedType);
		}

		void AddClicked(object sender, EventArgs e)
		{
			switch (_selectedType)
			{
				case SelectedElementType.Polyline:
					Map.MapElements.Add(_polyline = new Maps.Polyline());
					break;
				case SelectedElementType.Polygon:
					Map.MapElements.Add(_polygon = new Maps.Polygon());
					break;
				case SelectedElementType.Circle:
					Map.MapElements.Add(_circle = new Circle());
					break;
			}
		}

		void RemoveClicked(object sender, EventArgs e)
		{
			switch (_selectedType)
			{
				case SelectedElementType.Polyline:
					Map.MapElements.Remove(_polyline);
					_polyline = Map.MapElements.OfType<Maps.Polyline>().LastOrDefault();

					if (_polyline == null)
						Map.MapElements.Add(_polyline = new Maps.Polyline());

					break;
				case SelectedElementType.Polygon:
					Map.MapElements.Remove(_polygon);
					_polygon = Map.MapElements.OfType<Maps.Polygon>().LastOrDefault();

					if (_polygon == null)
						Map.MapElements.Add(_polygon = new Maps.Polygon());

					break;
				case SelectedElementType.Circle:
					Map.MapElements.Remove(_circle);
					_circle = Map.MapElements.OfType<Circle>().LastOrDefault();

					if (_circle == null)
						Map.MapElements.Add(_circle = new Circle());

					break;
			}
		}

		void ChangeColorClicked(object sender, EventArgs e)
		{
			var newColor = new Color((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());
			switch (_selectedType)
			{
				case SelectedElementType.Polyline:
					_polyline.StrokeColor = newColor;
					break;
				case SelectedElementType.Polygon:
					_polygon.StrokeColor = newColor;
					break;
				case SelectedElementType.Circle:
					_circle.StrokeColor = newColor;
					break;
			}
		}

		void ChangeWidthClicked(object sender, EventArgs e)
		{
			var newWidth = _random.Next(1, 50);
			switch (_selectedType)
			{
				case SelectedElementType.Polyline:
					_polyline.StrokeWidth = newWidth;
					break;
				case SelectedElementType.Polygon:
					_polygon.StrokeWidth = newWidth;
					break;
				case SelectedElementType.Circle:
					_circle.StrokeWidth = newWidth;
					break;
			}
		}

		void ChangeFillClicked(object sender, EventArgs e)
		{
			var newColor = new Color((float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble(), (float)_random.NextDouble());
			switch (_selectedType)
			{
				case SelectedElementType.Polygon:
					_polygon.FillColor = newColor;
					break;
				case SelectedElementType.Circle:
					_circle.FillColor = newColor;
					break;
			}
		}
	}
}