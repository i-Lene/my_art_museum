import { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [message, setMessage] = useState("");

  useEffect(() => {
    fetch("http://localhost:5186/api/hello")
      .then((res) => res.json())
      .then((data) => setMessage(data.message));
  }, []);


  return (
    <div>
      <h1>React + .NET App</h1>
      <p>{message}</p>
    </div>
  );
}

export default App;
