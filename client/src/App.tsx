import { Route, Routes } from "react-router";
import { LandingPage } from "./LandingPage";
import { Lobby } from "./Lobby";
import { NotFound } from "./NotFound";

function App() {
  return <Routes>
    <Route path="/" element={<LandingPage />} />
    <Route path="/lobby/:lobbyId" element={<Lobby />} />
    <Route path="*" element={<NotFound />} />
  </Routes>
}

export default App;
