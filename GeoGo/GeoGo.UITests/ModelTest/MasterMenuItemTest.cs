using NUnit.Framework;
using GeoGo.Model;
using System;
using Xamarin.Forms;

namespace GeoGo.Tests.ModelTest
{
    [TestFixture()]
    public class MasterMenuItemTest
    {
        MasterMenuItem item;

        [SetUp]
        public void SetUp(){
            item = new MasterMenuItem("Title", "Source", Color.Blue, typeof(Page) );
        }

        [Test()]
        public void MustHaveTitle()
        {
            Assert.AreEqual(item.Title,"Title");
        }

        [Test()]
        public void MustHaveSource()
        {
            Assert.AreEqual(item.IconSource, "Source");
        }

        [Test()]
        public void MustHaveColor(){
            Assert.IsInstanceOf(typeof(Color), item.BackgroundColor);
        }

        //[Test()]
        //public void MustHavePage()
        //{
        //    Assert.IsInstanceOf(typeof(Page), item.targetType);
        //}
    }
}
