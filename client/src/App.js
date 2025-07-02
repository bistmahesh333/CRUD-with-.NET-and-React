import React, { useState } from 'react';

function App() {
  const [id, setId] = useState('');
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchSignupData = async () => {
    if (!id) return;
    setLoading(true);

    try {
      const response = await fetch(`https://localhost:7077/api/Signup/GetSignup?id=${id}`);
      const result = await response.json();
      setData(result);
    } catch (error) {
      console.error('Error fetching signup data:', error);
      setData([]);
    }

    setLoading(false);
  };

  return (
    <div style={{ padding: '2rem' }}>
      <h1>Signup Viewer</h1>

      <div style={{ marginBottom: '1rem' }}>
        <input
          type="number"
          value={id}
          onChange={(e) => setId(e.target.value)}
          placeholder="Enter ID"
        />
        <button onClick={fetchSignupData} style={{ marginLeft: '10px' }}>
          Fetch
        </button>
      </div>

      {loading && <p>Loading...</p>}

      {!loading && data.length > 0 && (
        <table border="1" cellPadding="10">
          <thead>
            <tr>
              {Object.keys(data[0]).map((col) => (
                <th key={col}>{col}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.map((row, i) => (
              <tr key={i}>
                {Object.values(row).map((val, j) => (
                  <td key={j}>{val === null ? '' : val.toString()}</td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {!loading && data.length === 0 && id && <p>No data found.</p>}
    </div>
  );
}

export default App;
