import { IonButton, IonCard, IonLabel, IonTextarea } from "@ionic/react";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { INIT_MAP } from "../redux/slices/mapSlice";
import { RootState } from "../redux/store";
import { MapComponent } from "./MapComponent";

export const ComputeComplexe: React.FC = () => {
    const dispatch = useDispatch();
    const state = useSelector((s: RootState) => s.map);

    const [error, setError] = useState<string>();
    const [command, setCommand] = useState<string>();

    const callComputeMap = () => {
        if (state.id != '' && !state.finished) return;
        if ((command?.length ?? 0) == 0) {
            setError('Please enter a command'); return;
        }

        dispatch(INIT_MAP({command: command ?? ''}));
    }

    return (
        <div style={{display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column'}}>
            <IonCard>
                <IonTextarea placeholder="Commands ..." autoGrow={true} value={command}
                    onIonChange={e => setCommand(e.detail.value!)} style={{minWidth: '500px'}}/>
                <IonLabel>{error}</IonLabel>
            </IonCard>

            <IonButton onClick={callComputeMap}>
                <IonLabel>Compute</IonLabel>
            </IonButton><br/>

            <MapComponent map={state.map} />
        </div>
    );
}