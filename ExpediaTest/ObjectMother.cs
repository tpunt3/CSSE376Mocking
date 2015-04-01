using Expedia;
using System;

namespace ExpediaTest
{
	public class ObjectMother
	{
		public static Car BMW(){
            return new Car(33) {Name = "BMW M3"};
        }
	}
}
