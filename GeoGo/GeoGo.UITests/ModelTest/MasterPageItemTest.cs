using NUnit.Framework;
using GeoGo.Model;
using Xamarin.Forms;

namespace GeoGo.Tests.ModelTest
{
    [TestFixture()]
    public class MasterPageItemTest
    {
        MasterPageItem pageItem;

        [SetUp]
        public void SetUp()
        {
            pageItem = new MasterPageItem("title", "icon", typeof(Page));
        }

        [Test()]
        public void MustHavePageName(){
            Assert.IsNotEmpty(pageItem.Title);
        }

        [Test()]
        public void MustHaveIcon()
        {
            Assert.IsNotEmpty(pageItem.Title);
        }

        //[Test()]
        //public void MustHavePageType()
        //{
        //    //Assert.IsInstanceOfType(typeof Page, pageItem.targetType);
        //}
    }
}
