export function Search({ handleSearchSubmit, inputValue, handleSearchChange }) {
  return (
    <form>
      <input
        type="text"
        placeholder="Search artworks..."
        value={inputValue}
        onChange={handleSearchChange}
      />
      <button type="button" onClick={handleSearchSubmit}>
        Search
      </button>
    </form>
  );
}
