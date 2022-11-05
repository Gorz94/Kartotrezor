import { IonLabel } from "@ionic/react";
import { Map } from "../model/Map";

export interface MapProps {
    map: Map | undefined
}

export const MapComponent : React.FC<MapProps> = (props) => {
    return (
        props.map ? 
        (
            <>
                <IonLabel>Map: {props.map.width} x {props.map.height}</IonLabel>
            </>
        )
         : (<IonLabel>Map is undefined.</IonLabel>)
    )
}