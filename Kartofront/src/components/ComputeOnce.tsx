import { IonButton, IonLabel, IonTextarea } from "@ionic/react";
import { useState } from "react";
import { Map } from '../model/Map';
import { computeMap } from "../redux/services/mapService";
import { MapComponent } from "./MapComponent";

export const ComputeOnce: React.FC = () => {
    const [map, setMap] = useState<Map>();
    const [error, setError] = useState<string>();
    const [command, setCommand] = useState<string>();

    const callComputeMap = async () => {
        if  ((command?.length ?? 0) > 0) {
            setError('');

            try {
                const result = await computeMap(command ?? '');

                if (result.error) {
                    setError(result.error);
                } else {
                    setMap(result.map);
                }
            } catch (e)
            {
                setError(JSON.stringify(e));
            }
        } else {
            setError('You need at least one command.');
        }
    }

    return (
        <>
            <IonTextarea placeholder="Commands ..." autoGrow={true} value={command}
                onIonChange={e => setCommand(e.detail.value!)} />
            <IonLabel>{error}</IonLabel>

            <IonButton onClick={callComputeMap}>
                <IonLabel>Compute</IonLabel>
            </IonButton>

            <MapComponent map={map} />
        </>
    );
}