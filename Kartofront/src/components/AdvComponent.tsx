import { IonLabel } from '@ionic/react';
import Pikachu from '../img/Pikachu.png';
import { Direction } from '../model/Enum';

export interface AdvProps {
    name: string;
    direction: Direction;
    treasures: number;
}

export const AdvComponent : React.FC<AdvProps> = (props) => {
    const angle = [Direction.S, Direction.W, Direction.N, Direction.E].indexOf(props.direction) * 90;

    return (
        <>
            <img src={Pikachu} style={{transform: `rotate(${angle}deg)`, width: '60px', height: '60px'}}/>
            <IonLabel  style={{fontSize: '16px', fontWeight: 'bold', color: 'white',
            marginLeft: '5px', marginTop: '5px'}}>{props.name} ({props.treasures})</IonLabel>
        </>
    );
}