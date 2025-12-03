import { useState, useEffect } from "react";
import "./App.scss";
import { Button } from "./components/Button/Button";
import { Artwork } from "./components/Artwork/Artwork";
import {
  fetchMuseumData,
  changePage,
  addToFavourites,
  getFavouritesByUserId,
  removeFromFavourites,
  removeAllFavourites,
} from "./utils/fetchDataHandler";
import { Search } from "./components/Search/Search";

function App() {
  const [museumData, setMuseumData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [inputValue, setInputValue] = useState("");
  const [page, setPage] = useState(1);
  const [isFilteringByArtist, setIsFilteringByArtist] = useState(false);
  const [selectedArtistId, setSelectedArtistId] = useState(null);
  const [userId, setUserId] = useState(1);
  const [favourites, setFavourites] = useState([]);

  const desiredArtistsId = {
    "Pablo Picasso": 36198,
    "Henri Matisse": 35670,
    "Leonardo da Vinci": 67799,
    "Vincent van Gog": 40610,
    "Edvard Munch": 44014,
    "Claude Monet": 35809,
    "Salvador Dalí": 34123,
    "Katsushika Hokusai": 31492,
    "Georgia O'Keeffe": 36062,
    "Joan Miró": 32048,
  };

  useEffect(() => {
    fetchMuseumData(1, setLoading, setMuseumData, searchTerm);
  }, [searchTerm]);

  useEffect(() => {
    async function fetchFavs() {
      const data = await getFavouritesByUserId(userId);
      setFavourites(data);
    }

    fetchFavs();
  }, [userId]);

  const artworks = museumData?.data || [];
  const pagination = museumData?.pagination || {};
  const { current_page, total_pages } = pagination;

  const handleSearchChange = (e) => {
    setInputValue(e.target.value);
  };

  function handleSearchSubmit(event) {
    event.preventDefault();
    setSearchTerm(inputValue);
    setPage(1);
  }

  function handleFilterByArtistChange(event) {
    const isChecked = event.target.checked;
    const artistValue = parseInt(event.target.value, 10);

    if (isChecked) {
      setIsFilteringByArtist(true);
      setSelectedArtistId(artistValue);

      fetchMuseumData(
        1,
        setLoading,
        setMuseumData,
        searchTerm,
        true,
        artistValue
      );
    } else {
      setIsFilteringByArtist(false);
      setSelectedArtistId(null);
      fetchMuseumData(1, setLoading, setMuseumData, searchTerm);
    }
  }

  function handleAddToFavourites(artworkId) {
    addToFavourites(userId, artworkId);
    const artworkObj = artworks.find((art) => art.id === artworkId);
    if (artworkObj) {
      setFavourites((prev) => [...prev, artworkObj]);
    }
  }

  function handleRemoveFromFavs(artworkId) {
    removeFromFavourites(userId, artworkId).then(() => {
      setFavourites((prev) => prev.filter((fav) => fav.id !== artworkId));
    });
  }

  function handleRemoveAllFavourites() {
    removeAllFavourites(1);
    setFavourites([]);
  }

  const isFavourite = (artworkId) => {
    return favourites.some((fav) => fav.id === artworkId);
  };

  return (
    <div>
      <div>
        <Search
          handleSearchChange={handleSearchChange}
          inputValue={inputValue}
          handleSearchSubmit={handleSearchSubmit}
        />
        <div>
          {Object.entries(desiredArtistsId).map(([artistName, artistId]) => (
            <div key={artistId}>
              <input
                type="checkbox"
                id={`filterByArtist-${artistId}`}
                checked={selectedArtistId === artistId}
                onChange={handleFilterByArtistChange}
                value={artistId}
              />

              <label htmlFor={`filterByArtist-${artistId}`}>
                Filter by Artist: {artistName}
              </label>
            </div>
          ))}
        </div>

        {loading && <p>Loading...</p>}
        {!loading &&
          artworks.map((artwork) => (
            <Artwork
              key={artwork.id}
              artwork={artwork}
              handleAddToFavourites={handleAddToFavourites}
              isFav={isFavourite(artwork.id)}
              handleRemoveFromFavs={handleRemoveFromFavs}
            />
          ))}

        {!loading && museumData && (
          <div className="pagination-controls">
            <Button
              onClick={() =>
                changePage(
                  "previous",
                  museumData,
                  setLoading,
                  setMuseumData,
                  searchTerm,
                  isFilteringByArtist,
                  selectedArtistId
                )
              }
              disabled={current_page === 1}
            >
              Previous
            </Button>

            <Button
              onClick={() =>
                changePage(
                  "next",
                  museumData,
                  setLoading,
                  setMuseumData,
                  searchTerm,
                  isFilteringByArtist,
                  selectedArtistId
                )
              }
              disabled={current_page === total_pages}
            >
              Next
            </Button>

            <span>
              Page {current_page} of {total_pages}
            </span>
          </div>
        )}
      </div>
      <div>
        <h2>Favourites</h2>
        {favourites.length === 0 && <p>No favourites yet.</p>}
        {favourites.length > 0 && (
          <div>
            <button onClick={handleRemoveAllFavourites}>Remove All </button>
            <ul>
              {favourites.map((fav) => (
                <Artwork
                  key={fav.id}
                  artwork={fav}
                  isFav={isFavourite(fav.id)}
                  handleAddToFavourites={handleAddToFavourites}
                  handleRemoveFromFavs={handleRemoveFromFavs}
                />
              ))}
            </ul>
          </div>
        )}
      </div>
    </div>
  );
}

export default App;
