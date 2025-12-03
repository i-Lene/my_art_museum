export function Artwork({
  artwork,
  handleAddToFavourites,
  isFav = false,
  handleRemoveFromFavs,
}) {
  return (
    <div className="artwork-card">
      <h2>{artwork.title}</h2>
      {artwork.artist_titles && (
        <p>
          <strong>Artist:</strong> {artwork.artist_titles.join(", ")}
        </p>
      )}
      <img src={artwork.full_Img_Url} alt={artwork.alt_Text} />
      {!isFav && (
        <button onClick={() => handleAddToFavourites(artwork.id)}>
          Add to Favourites
        </button>
      )}

      {isFav && (
        <button onClick={() => handleRemoveFromFavs(artwork.id)}>
          Remove from favourites
        </button>
      )}
    </div>
  );
}
