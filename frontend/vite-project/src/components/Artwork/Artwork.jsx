export function Artwork({ artwork }) {
  return (
    <div className="artwork-card">
      <h2>{artwork.title}</h2>
      {artwork.artist_titles && (
        <p>
          <strong>Artist:</strong> {artwork.artist_titles.join(", ")}
        </p>
      )}
      <img src={artwork.full_Img_Url} alt={artwork.alt_Text} />
    </div>
  );
}
