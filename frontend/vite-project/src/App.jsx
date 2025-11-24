import { useState, useEffect } from "react";
import "./App.scss";
import { Button } from "./components/Button/Button";
import { Artwork } from "./components/Artwork/Artwork";
import { fetchMuseumData, changePage } from "./utils/fetchDataHandler";

function App() {
  const [museumData, setMuseumData] = useState(null);
  const [loading, setLoading] = useState(false);
  const [searchTerm, setSearchTerm] = useState("");
  const [inputValue, setInputValue] = useState("");
  const [page, setPage] = useState(1);

  useEffect(() => {
    fetchMuseumData(1, setLoading, setMuseumData, searchTerm);
  }, [searchTerm]);

  const artworks = museumData?.data || [];
  const pagination = museumData?.pagination || {};
  const { current_page, total_pages } = pagination;

  console.log(museumData);

  const handleSearchChange = (e) => {
    setInputValue(e.target.value);
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    setSearchTerm(inputValue);
    setPage(1);
  };

  return (
    <div>
      <form onSubmit={handleSearchSubmit}>
        <input
          type="text"
          placeholder="Search artworks..."
          value={inputValue}
          onChange={handleSearchChange}
        />
        <button type="submit">Search</button>
      </form>

      {loading && <p>Loading...</p>}
      {!loading &&
        artworks.map((artwork) => (
          <Artwork key={artwork.id} artwork={artwork} />
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
                searchTerm
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
                searchTerm
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
  );
}

export default App;
