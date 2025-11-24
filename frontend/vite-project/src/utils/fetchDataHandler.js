const API_BASE = "http://localhost:5186/api/museumdata";
const LIMIT = 10;

export async function fetchMuseumData(
  page = 1,
  setLoading,
  setMuseumData,
  searchTerm = ""
) {
  try {
    setLoading(true);

    const url = searchTerm
      ? `${API_BASE}/search?q=${encodeURIComponent(
          searchTerm
        )}&page=${page}&limit=${LIMIT}`
      : `${API_BASE}?page=${page}&limit=${LIMIT}`;

    console.log(url);

    const res = await fetch(url);

    if (!res.ok) {
      throw new Error(`Server returned ${res.status}`);
    }

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
  searchTerm
) {
  if (!museumData) return;

  const { current_page, total_pages } = museumData.pagination;

  const nextPage =
    direction === "previous" ? current_page - 1 : current_page + 1;

  if (nextPage < 1 || nextPage > total_pages) return;

  fetchMuseumData(nextPage, setLoading, setMuseumData, searchTerm);
}
