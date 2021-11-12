import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
    buildGameSystem
} from '../store/gamesystem/gamesystem.slice';

export default function GameSystem() {
    console.log('Enter GameSystem');

    const gameSystem = useSelector(buildGameSystem)

    console.log('GameSystem data', gameSystem);

    // const dispatch = useDispatch();

    return (
        <div>
            {gameSystem && <span>{gameSystem.createdDTTM}</span>}
        </div>
    );
}
