using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
	public class Person
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }

		public static Person[] People =
		{
			new Person{ID=1, FirstName="Pera", LastName="Perić", Address="Vojvode Stepe 123"},
			new Person{ID=2, FirstName="Mika", LastName="Mikić", Address="Beogradski Kej 456"},
			new Person{ID=3, FirstName="Žika", LastName="Žikić", Address="Modene 5"},
			new Person{ID=4, FirstName="Nataša", LastName="Stefanović", Address="Cara Dušana 111"},
			new Person{ID=5, FirstName="Stefan", LastName="Jovanović", Address="Jovana Jovanovića Zmaja 77"},
			new Person{ID=6, FirstName="Marija", LastName="Marić", Address="Živojina Ćuluma 10"},
			new Person{ID=7, FirstName="Marko", LastName="Marić", Address="Živojina Ćuluma 10"},
			new Person{ID=8, FirstName="Laza", LastName="Lazić", Address="Laze Telečkog 123"},
			new Person{ID=9, FirstName="Gordana", LastName="Krstić", Address="Kosovska 456"},
			new Person{ID=10, FirstName="Stefanija", LastName="Milkić", Address="Šumadijska 963"},
		};
	}
}
