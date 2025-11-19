import { useState, useEffect } from "react";

import "./App.scss";

function App() {
  const [museumData, setMuseumData] = useState(null);

  useEffect(() => {
    fetch("http://localhost:5186/api/museumdata")
      .then((res) => res.json())
      .then((data) => setMuseumData(data))
      .catch((error) => console.error("Error fetching museum data:", error));
  }, []);

  const artworks = museumData ? museumData.data : [];

  console.log("Artworks:", artworks);

  return (
    <div>
      {artworks ? (
        <div>
          {artworks.map((artwork) => {
            const iiifBase = "https://www.artic.edu/iiif/2";
            const imageId = artwork.image_id;
            const imageUrl = artwork.thumbnail ? `${iiifBase}/${imageId}/full/${artwork.thumbnail.width},/0/default.jpg` : "https://placehold.co/600x400?text=No+Image";

            return (
              <div key={artwork.id}>
                <p>{artwork.title} {artwork.date_display}</p>
                <img src={imageUrl} alt={artwork.thumbnail?.alt_text} />
                <p>
                  {artwork.artist_titles
                    ? artwork.artist_titles.map((artist) => artist)
                    : ""}
                </p>

              </div>
            );
          })}
        </div>
      ) : (
        <p>Loading museum data...</p>
      )}
    </div>
  );
}

export default App;