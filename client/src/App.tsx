function App() {

  const clickJoinLobby = () => { };

  return (
    <div className="min-h-screen text-center bg-trueGray-900">
      <div className="py-16">
        <h1 className="text-9xl text-red-700 font-medium uppercase tracking-wider font-serif">Deceit</h1>
        <h2 className="text-4xl text-red-700 font-extralight uppercase tracking-widest">Homicide in Hong Kong</h2>
        <p className="pt-6 text-lightBlue-200">
          An online implementation of <a href="https://greyfoxgames.com/deception-murder-in-hong-kong/" target="_blank" rel="noreferrer">Deception: Murder in Hong Kong</a>.
        </p>
      </div>
      <div className="py-16 space-y-8">
        <div>
          <a className="btn bg-lightBlue-500 text-black hover:bg-lightBlue-400" href="/lobby">Create Lobby</a>
        </div>
        <div>
          <input className="block bg-lightBlue-100 text-black font-semibold rounded-md p-2 mx-auto text-center placeholder-trueGray-600" placeholder="Lobby Code" />
          <button className="btn bg-lightBlue-500 text-black hover:bg-lightBlue-400" onClick={clickJoinLobby}>Join Lobby</button>
        </div>
      </div>
    </div >
  );
}

export default App;
