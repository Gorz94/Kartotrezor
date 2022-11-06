import { IonLabel } from "@ionic/react";

export interface TreasureProps {
    count: number;
}

export const TreasureComponent : React.FC<TreasureProps> = (props) => {
    return (
        <IonLabel style={{fontSize: '16px', fontWeight: 'bold', color: 'white',
            marginLeft: '5px', marginTop: '5px'}}>Treasures: {props.count}</IonLabel>
    );
}