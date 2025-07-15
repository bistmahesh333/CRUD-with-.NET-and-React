import React, { useState, useEffect } from 'react';
import axios from 'axios';

function AddUsers() {
  const [items, setItems] = useState([]); // List of items from API
  const [name, setName] = useState('');   // Input value

  // Fetch all items (GET)
  const fetchItems = async () => {
    try {
      const res = await axios.get('https://your-api-url/api/items');
      setItems(res.data);
    } catch (error) {
      console.error('Error fetching items:', error);
    }
  };

    // Fetch all items on component mount
  useEffect(() => {
    fetchItems();
  }, []);

  // Add new item (POST)
  const addItem = async (e) => {
    e.preventDefault(); // Prevent form submit reload
    try {
      await axios.post('https://your-api-url/api/items', { name });
      setName('');     // Clear input after add
      fetchItems();    // Refresh list after adding
    } catch (error) {
      console.error('Error adding item:', error);
    }
  };

  return (
    <div>
      <h2>Add Item</h2>
      <form onSubmit={addItem}>
        <input
          type="text"
          placeholder="Enter item name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
        <button type="submit">Add</button>
      </form>

      <h3>All Items</h3>
      <ul>
        {items.map((item) => (
          <li key={item.id}>{item.name}</li>
        ))}
      </ul>
    </div>
  );
}

export default AddUsers;
