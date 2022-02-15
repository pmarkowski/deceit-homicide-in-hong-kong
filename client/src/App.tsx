import { Route, Routes } from "react-router";
import { LandingPage } from "./LandingPage";
import { PageLayout } from "./PageLayout";
import { Lobby } from "./GameLobby";
import { NotFound } from "./NotFound";

function App() {
  return <Routes>
    <Route element={<PageLayout />}>
      <Route path="/" element={<LandingPage />} />
      <Route path="/lobby/:lobbyId" element={<Lobby />} />
      <Route path="*" element={<NotFound />} />
    </Route>
  </Routes>
}

export default App;
