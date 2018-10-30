using NUnit.Framework;
using System;
using GeoGo.Model;
using GeoGo.styles;
using Xamarin.Forms;

namespace GeoGo.UITests.ModelTest
{
    [TestFixture()]
    public class UIColor_list_Test
    {
        ColorItem colorItem;

        [SetUp]
        public void SetUp()
        {
            colorItem = new ColorItem() { Color = Color.Gold, Name = "Gold" };
        }

        [Test()]
        public void CanDeclareNewColorItem()
        {
            Assert.IsInstanceOf( typeof (Color), colorItem.Color);
            Assert.IsNotNullOrEmpty(colorItem.Name);
        }

        [Test()]
        public void CanGenerateRandomUIColor(){
            Assert.IsInstanceOf(typeof(Color), UIColor_list.GetRandomColor());
        }

        [Test()]
        public void CanGetUIColorByNumber(){
            Assert.IsInstanceOf(typeof(Color), UIColor_list.GetColorByNumber(10) );
        }

        [Test()]
        public void CanDisplayTheNumbersOfColorOntheList()
        {
            Assert.Greater(UIColor_list.fullColorList.Count, 20);
        }
    }
}
