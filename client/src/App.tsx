function App() {

  const clickCreateLobby = () => { };

  return (
    <div className="min-h-screen text-center bg-trueGray-900">
      <div className="py-16">
        <h1 className="text-9xl text-red-700 font-medium uppercase tracking-wider font-serif">Deceit</h1>
        <h2 className="text-4xl text-red-700 font-extralight uppercase tracking-widest">Homicide in Hong Kong</h2>
        <p className="pt-6 text-lightBlue-200">
          An online implementation of <a href="https://greyfoxgames.com/deception-murder-in-hong-kong/" target="_blank" rel="noreferrer">Deception: Murder in Hong Kong</a>.
        </p>
      </div>
      <div className="py-16 space-y-8 w-1/6 mx-auto">
        <div>
          <button className="btn bg-lightBlue-300 text-trueGray-800 hover:bg-lightBlue-400 w-full" onClick={clickCreateLobby}>Create Lobby</button>
        </div>
      </div>
    </div >
  );
}

export default App;
