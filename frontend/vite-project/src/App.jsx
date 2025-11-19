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
  

  console.log(museumData);

  return <div>{museumData?.map(artwork => <p key={artwork.id}>{artwork.title ?? ""}</p>)}</div>;
}

export default App;
