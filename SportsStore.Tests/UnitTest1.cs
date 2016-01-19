using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace SportsStore.Tests
{
    [TestClass]
    public class RouteTests
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null,string httpMethod="GET")
        {
            Mock<HttpRequestBase> mock = new Mock<HttpRequestBase>();
            mock.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);
            mock.Setup(m => m.HttpMethod).Returns(httpMethod);

            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mock.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            return mockContext.Object;
        }

        private void TestRouteMatch(string url, string controller, string action, object routeProperties = null, string httpMethod = "GET")
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            RouteData result = routes.GetRouteData(CreateHttpContext(url,httpMethod));

            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result,controller,action,routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult,string controller,string action,object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) => { return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0; };

            bool result = valCompare(routeResult.Values["controller"],controller) && valCompare(routeResult.Values["action"],action);

            if(propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (var pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name) && valCompare(routeResult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            Assert.IsTrue(result == null || result.Route == null);
        }
        [TestMethod]
        public void TestIncomingRoutes(){
            TestRouteMatch("~/Admin/Index","Admin","Index");
            TestRouteMatch("~/One/Two","One","Two");
            TestRouteFail("~/Admin/Index/Segment/abc");
            //TestRouteFail("~/Admin");
        }

    }
}
