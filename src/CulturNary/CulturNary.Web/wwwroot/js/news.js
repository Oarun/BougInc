const NewsArticles = {
    searchRecipes: function (query) {
        const url = `/api/NewsApi/search?q=${encodeURIComponent(query)}`;

        return fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .catch(error => {
                console.error('Fetch error:', error);
                throw new Error('Failed to load recipes.');
            });
    },

    displayResults: function (data) {
        console.log(data);
        const resultsDiv = document.getElementById('newsContainer');
        resultsDiv.innerHTML = ''; // Clear previous results

        if (data.articles && data.articles.length > 0) {
            const row = document.createElement('div');
            row.className = 'row d-flex justify-content-evenly card-rows'; // Bootstrap row

            data.articles.forEach(article => {
                // Check if the article title contains '[Removed]'
                if (!article.title.includes('[Removed]')) {
                    const col = document.createElement('div');
                    col.className = 'col-lg-5 col-md-5 col-sm-5 mb-5'; // Bootstrap column, adjust 'md-4' to change number of columns per row

                    const card = document.createElement('div');
                    card.className = 'article-card';
                    const publishedDate = new Date(article.publishedAt);
                    const formattedDate = publishedDate.toLocaleDateString('en-US', {
                        year: 'numeric',
                        month: 'long',
                        day: 'numeric'
                    });
                    card.style.backgroundColor = '#fff';
                    card.style.borderRadius = '10px';
                    card.style.boxShadow = '0 4px 8px 0 rgba(0,0,0,0.2)';
                    card.style.height = '55s0px';
                    const description = truncateText(article.description, 140); // Assuming truncateText function is defined elsewhere
                    card.innerHTML = `
                    <img src="${article.urlToImage}" class="card-img-top p-3" alt="Article Image">
                    <div class="card-article-body pt-1 text-dark pb-3">
                        <h4 class="card-article-title">${article.title}</h4>
                        <h6 class="text-muted card-article-author">| Published on ${formattedDate}</h6>
                        <p class="card-article-description">${description}</p>
                        <a href="${article.url}" target="_blank" class="card-article-link mb-4 pb-2 mt-auto">Read More >> </a>
                    </div>
                `;

                    col.appendChild(card);
                    row.appendChild(col);
                }
            });

            resultsDiv.appendChild(row);
        } else {
            resultsDiv.innerHTML = '<p>No news articles found.</p>';
        }
    }
};

document.getElementById('categoryForm').addEventListener('submit', function (event) {
    event.preventDefault();
    const query = document.getElementById('categorySelect').value;
    NewsArticles.searchRecipes(query).then(NewsArticles.displayResults).catch(error => {
        console.error(error);
        document.getElementById('newsContainer').innerHTML = 'Error loading news.';
    });
});

function truncateText(text, maxLength) {
    return text.length > maxLength ? text.substring(0, maxLength - 3) + '...' : text;
}
