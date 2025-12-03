const API_BASE = import.meta.env.VITE_API_BASE;
const FAVOURITES_API_BASE = import.meta.env.VITE_FAVOURITES_API_BASE;

const LIMIT = 10;

export async function fetchMuseumData(
  page = 1,
  setLoading,
  setMuseumData,
  searchTerm = "",
  filterByartistId = null,
  artistId = null
) {
  try {
    setLoading(true);

    let url;

    if (searchTerm && filterByartistId && artistId) {
      url = `${API_BASE}/artist/${artistId}/search?q=${encodeURIComponent(
        searchTerm
      )}&page=${page}&limit=${LIMIT}`;
    } else if (searchTerm) {
      url = `${API_BASE}/search?q=${encodeURIComponent(
        searchTerm
      )}&page=${page}&limit=${LIMIT}`;
    } else if (filterByartistId && artistId) {
      url = `${API_BASE}/artist/${artistId}?page=${page}&limit=${LIMIT}`;
    } else {
      url = `${API_BASE}?page=${page}&limit=${LIMIT}`;
    }

    const res = await fetch(url);
    if (!res.ok) throw new Error(`Server returned ${res.status}`);

    const data = await res.json();
    setMuseumData(data);
  } catch (err) {
    console.error("Error fetching data:", err);
  } finally {
    setLoading(false);
  }
}

export function changePage(
  direction,
  museumData,
  setLoading,
  setMuseumData,
  searchTerm,
  filterByartistId,
  artistId
) {
  if (!museumData) return;

  const { current_page, total_pages } = museumData.pagination;

  const nextPage =
    direction === "previous" ? current_page - 1 : current_page + 1;

  if (nextPage < 1 || nextPage > total_pages) return;

  fetchMuseumData(
    nextPage,
    setLoading,
    setMuseumData,
    searchTerm,
    filterByartistId,
    artistId
  );
}

export function addToFavourites(userId, artworkId) {
  const url = `${FAVOURITES_API_BASE}`;
  const method = "POST";
  const params = { userId, artworkId };
  const errorMessage = "Error adding to favourites1";

  return handleResponse(url, method, params, errorMessage);
}

export function getFavouritesByUserId(userId) {
  const url = `${FAVOURITES_API_BASE}/${userId}`;
  const method = "GET";
  const errorMessage = "Error fetching favourites:";

  return handleResponse(url, method, null, errorMessage);
}

export function removeFromFavourites(userId, artworkId) {
  const url = `${FAVOURITES_API_BASE}/removefavourite/1/${artworkId}`;
  const method = "DELETE";
  const params = { userId, artworkId };
  const errorMessage = "Error removing favourite";

  return handleResponse(url, method, params, errorMessage);
}

export function removeAllFavourites(userId) {
  const url = `${FAVOURITES_API_BASE}/removeallfavourites/${userId}`;
  const method = "DELETE";
  const params = { userId };
  const errorMessage = "Error removing all favourites";

  return handleResponse(url, method, params, errorMessage);
}

async function handleResponse(url, method = "GET", params, errorMessage) {
  try {
    const response = await fetch(url, {
      method: method,
      headers: {
        "Content-Type": "application/json",
      },
      body: method != "GET" ? JSON.stringify({ ...params }) : null,
    });
    if (!response.ok) {
      throw new Error(`Server returned ${response.status}`);
    }
    const data = await response.json();
    return data;
  } catch (error) {
    console.error(errorMessage, error);
    throw error;
  }
}
