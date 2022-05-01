import { Route, Routes } from "react-router";
import { LandingPage } from "./LandingPage";
import { PageLayout } from "./PageLayout";
import { GameLobby } from "./GameLobby";
import { NotFound } from "./NotFound";
import { GamePreview } from "./GamePreview";

function App() {
  return <Routes>
    <Route element={<PageLayout />}>
      <Route path="/" element={<LandingPage />} />
      <Route path="/lobby/:lobbyId" element={<GameLobby />} />
      <Route path="/game-preview" element={<GamePreview />} />
      <Route path="*" element={<NotFound />} />
    </Route>
  </Routes>
}

export default App;
