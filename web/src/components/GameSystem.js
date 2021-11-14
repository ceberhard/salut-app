import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
    buildGameSystem
} from '../store/gamesystem/gamesystem.slice';
import styles from './GameSystem.css';

const SubComponents = ({ component }) => {
    if (!component || !component.children) {
        return <h2>Loading...</h2>;
    }

    return component && component.children && component.children.map((child) =>
        <li key={child.id}>{child.name} ({child.rollupPointValue || child.pointValue} points)</li>
    );
    
}

export default function GameSystem() {
    console.log('Enter GameSystem');

    const gameSystem = useSelector(buildGameSystem)

    console.log('GameSystem data', gameSystem);

    // const dispatch = useDispatch();

    const rootComponents = gameSystem && gameSystem.components && gameSystem.components.map((component) => 
        <div key={component.id}>
            <h3>{component.name} ({component.rollupPointValue} points)</h3>
            <ul>
                <SubComponents component={component} />
            </ul>
        </div>
    );


    return (
        <div className="gameSystem">
            {gameSystem && <div>
                <h1>{gameSystem.gameSystemId}</h1>
                {rootComponents}
            </div>
            }
        </div>
    );
}
