import styles from './Contact.module.css';
import { useSelector } from 'react-redux';
import {
    selectContactById
} from '../store/contacts/contacts.slice';

export const ContactItem = ({ id }) => {
    console.log('In ContactItem...')
    const contact = useSelector((state) => selectContactById(state, id));

    return (
        <div>
            <span className={styles.value}>{contact.lastName}, {contact.firstName}</span>
        </div>
    );
}

