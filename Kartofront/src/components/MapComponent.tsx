import { IonCol, IonGrid, IonLabel, IonRow } from "@ionic/react";
import { createEntity } from "../helpers/EntityFactory";
import { Level } from "../model/Enum";
import { Map } from "../model/Map";

export interface MapProps {
    map: Map | undefined
}

export const MapComponent : React.FC<MapProps> = (props) => {
    const range = (n: number) => Array.from(Array(n).keys());
    const mapWidth = Math.max(400, (window.innerWidth - 20) * (props.map?.width ?? 1)/ 12);

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

    const getEntities = (i: number, j: number) => {
        if (props.map) {
            return props.map.slots[i + j * props.map.width].entities;
        }

        return [];
    }

    return (
        props.map ? 
        (
            <>
                <IonGrid style={{minHeight: '100px', width: mapWidth, '--ion-grid-columns': props.map.width.toString()}}>
                {
                    range(props.map.height).map(j => (
                        <IonRow key={j} style={{minHeight: '100px'}}>
                        {
                            range(props.map?.width ?? 0).map(i => (
                                <IonCol key={i}>
                                    <div style={{backgroundColor: getColor(i, j), height: '100%', width: '100%'}}>
                                    {
                                        getEntities(i, j).map(e => createEntity(e))
                                    }
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