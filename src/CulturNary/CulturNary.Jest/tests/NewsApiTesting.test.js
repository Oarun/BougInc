const fetch = require("jest-fetch-mock");
jest.setMock("node-fetch", fetch);

const { NewsArticles } = require("../news");

describe("NewsArticles", () => {
    beforeEach(() => {
        fetch.resetMocks();
    });

    describe("searchRecipes", () => {
        it("fetches data and returns a JSON object", async () => {
            const mockResponse = {
                articles: [{ title: "Article 1" }, { title: "Article 2" }],
            };
            fetch.mockResponseOnce(JSON.stringify(mockResponse));

            const data = await NewsArticles.searchRecipes("test");
            expect(data).toEqual(mockResponse);
            expect(fetch).toHaveBeenCalledTimes(1);
            expect(fetch).toHaveBeenCalledWith("http://localhost/api/NewsApi/search?q=test");
        });

        it("throws an error if the network response is not ok", async () => {
            fetch.mockResponseOnce("Error", { status: 500 });

            await expect(NewsArticles.searchRecipes("test")).rejects.toThrow(
                "Network response was not ok"
            );
        });

        it("throws an error if fetch fails", async () => {
            fetch.mockReject(new Error("Failed to load recipes."));

            await expect(NewsArticles.searchRecipes("test")).rejects.toThrow(
                "Failed to load recipes."
            );
        });
    });
});
