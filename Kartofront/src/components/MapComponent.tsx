import { IonCol, IonGrid, IonLabel, IonRow } from "@ionic/react";
import { Level } from "../model/Enum";
import { Map } from "../model/Map";

export interface MapProps {
    map: Map | undefined
}

export const MapComponent : React.FC<MapProps> = (props) => {
    const range = (n: number) => Array.from(Array(n).keys());

    const getColor = (i: number, j: number) => {
        if (props.map) {
            const slot = props.map.slots[i + j * props.map.width];
            switch (slot.level) {
                case Level.Mountain: return 'brown';
                default: return 'green';
            }
        }

        return 'white';
    }

    return (
        props.map ? 
        (
            <>
                <IonGrid style={{minHeight: '100px', width: '100%', '--ion-grid-columns': props.map.width.toString()}}>
                {
                    range(props.map.height).map(j => (
                        <IonRow key={j} style={{minHeight: '100px'}}>
                        {
                            range(props.map?.width ?? 0).map(i => (
                                <IonCol key={i}>
                                    <div style={{backgroundColor: getColor(i, j), height: '100%', width: '100%'}}>

                                    </div>
                                </IonCol>
                            ))
                        }
                        </IonRow>
                    ))
                }
                </IonGrid>
            </>
        )
         : (<IonLabel>Map is undefined.</IonLabel>)
    )
}