using System.Net;
using System.Net.Http.Headers;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace CloudCustomers.UnitTests.Helpers;

public static class MockHttpMessageHandler<T>
{
    public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };
        
        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return handlerMock;
    }
}