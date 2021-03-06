import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import store from './store/store';
import { Provider } from 'react-redux';
import { fetchContacts } from './store/contacts/contacts.slice';
import { fetchGameSystem } from './store/gamesystem/gamesystem.slice';

store.dispatch(fetchContacts());
store.dispatch(fetchGameSystem());

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

