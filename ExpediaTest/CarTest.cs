using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatCarCanGetLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDB.getCarLocation(7)).Return("Driving around town");
            Expect.Call(mockDB.getCarLocation(14)).Return("On the beach");
            Expect.Call(mockDB.getCarLocation(1)).Return("In a volcano");

            mocks.ReplayAll();

            Car target = new Car(1000);
            target.Database = mockDB;

            String result;

            result = target.getCarLocation(7);
            Assert.AreEqual("Driving around town", result);

            result = target.getCarLocation(14);
            Assert.AreEqual("On the beach", result);

            result = target.getCarLocation(1);
            Assert.AreEqual("In a volcano", result);

            mocks.VerifyAll();

        }

        [TestMethod]
        public void TestThatCarCanGetMileageFromDatabase(){

            IDatabase mockData = mocks.StrictMock<IDatabase>();

            int miles = 1000000;

            Expect.Call(mockData.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockData.Miles = miles;

            var target = ObjectMother.BMW();

            target.Database = mockData;

            int mileCount = target.Mileage;

            Assert.AreEqual(miles, mileCount);

            mocks.VerifyAll();

        }

	}
}
