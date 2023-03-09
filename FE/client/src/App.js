import React from 'react';
import './App.css';

/*function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}
*/

function App() {
  const [word, setWord] = React.useState('Enter Shipper ID');
  const [associations, setAssociations] = React.useState(null);

  const getAssociations = () => {
    fetch('/api/getShipment/' + word)
      .then(result => result.json())
      .then(body => setAssociations(body));
  };

  return (
    
    <div className="app">
      <h1>Warehouse Shipping Report</h1>
      <input value={word} onChange={e => setWord(e.target.value)} />
      <button onClick={getAssociations}>Find Shipments</button>
      {associations && (
        Object.keys(associations).length === 0
          ? <p>No results</p>
          : <table>
            <tr><th>Shippment ID</th>
            <th>Warehouse ID</th>
            <th>Date</th>
            <th>Shipping PO</th>
            <th>Boxes Rcvd</th>
            <th>ShipperID</th></tr>
            {Object.entries(associations).map(item => (
              <tr><td>{item[1].ShipmentID}</td>
              <td> {item[1].WarehouseID} </td>
              <td> {new Date(item[1].Date).toDateString()} </td>
              <td> {item[1].ShippingPO} </td>
              <td> {item[1].BoxesRcvd} </td>
              <td> {item[1].ShipperID} </td>
              </tr>
            ))}
            </table>
      )}
    </div>
  );
}

export default App;
