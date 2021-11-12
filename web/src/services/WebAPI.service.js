import axios from "axios";

export async function getContacts() {
    console.log('In getContacts');
    const response = await axios.get('http://localhost:5000/api/contacts');
    return response.data;
}

export async function buildGame() {
    console.log('In buildGame');
    const response = await axios.post('http://localhost:5000/api/gamesystem/build/1001');
    console.log('buildGame data', response.data);
    return response.data;
}
