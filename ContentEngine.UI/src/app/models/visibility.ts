import { Clazz, getDefaultModelSchema, object, raw, serializable, serialize } from 'serializr';
import { v4 as uuidv4 } from 'uuid';
import { PropSchemaType } from './prop-schema-type';

export const OPERATOR_TYPE_ALWAYS = 'OperatorAlways';
export const OPERATOR_TYPE_NEVER = 'OperatorNever';
export const OPERATOR_TYPE_USERISINROLE = 'OperatorUserIsInRole'; 
export const OPERATOR_TYPE_USERISNOTINROLE = 'OperatorUserIsNotInRole';
export const OPERATOR_TYPE_AND = 'OperatorAnd';
export const OPERATOR_TYPE_OR = 'OperatorOr';
export const OPERATOR_TYPE_NOT = 'OperatorNot';
export const OPERATOR_TYPE_ISCHECKED = 'OperatorIsChecked';
export const OPERATOR_TYPE_ISNOTCHECKED = 'OperatorIsNotChecked';
export const OPERATOR_TYPE_EQUALS = 'OperatorEquals';
export const OPERATOR_TYPE_NOTEQUALS = 'OperatorNotEquals';
export const OPERATOR_TYPE_LIKE = 'OperatorLike';
export const OPERATOR_TYPE_INSET = 'OperatorInSet';
export const OPERATOR_TYPE_NOTINSET = 'OperatorNotInSet';

export abstract class Operator {
    @serializable _id: string;
    @serializable readonly abstract _type: string;
    
    public get id(): string {
        return this._id;
    }
    public abstract get name(): string;
    public abstract get isConfigured(): boolean;

    public abstract getResult(): boolean;

    constructor() {
        this._id = uuidv4();  // this is the default value if not deserialized
    }
}

// BOOLEAN
export abstract class BooleanOperator extends Operator {}

export class AlwaysOperator extends BooleanOperator {
    @serializable readonly _type = OPERATOR_TYPE_ALWAYS;
    public get name(): string { return 'Always'; }
    public get isConfigured(): boolean { return true; }
    
    public getResult(): boolean {
        return true;
    }
}

export class NeverOperator extends BooleanOperator {
    @serializable readonly _type = OPERATOR_TYPE_NEVER;
    public get name(): string { return 'Never'; }
    public get isConfigured(): boolean { return true; }

    public getResult(): boolean {
        return false;
    }
}

// ROLE (SECURITY)
export abstract class RoleBasedOperator extends Operator {
    @serializable _role?: string;
    public get isConfigured(): boolean { return this._role != null && this._role.trim() !== ''; }

    public get role(): string | undefined {
        return this._role;
    }
    public set role(role: string | undefined) {
        this._role = role;
    }
}

export class UserIsInRoleOperator extends RoleBasedOperator {
    @serializable readonly _type = OPERATOR_TYPE_USERISINROLE;
    public get name(): string { return 'User is in role'; }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

export class UserIsNotInRoleOperator extends RoleBasedOperator {
    @serializable readonly _type = OPERATOR_TYPE_USERISNOTINROLE;
    public get name(): string { return 'User is not in role'; }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

// LOGIC
export abstract class LogicOperator extends Operator {
    @serializable(getSerializableListOfOperators()) private _leftOperand?: Operator;
    @serializable(getSerializableListOfOperators()) private _rightOperand?: Operator;

    public get leftOperand(): Operator | undefined { 
        return this._leftOperand;
    }
    public set leftOperand(value: Operator | undefined) {
        this._leftOperand = value;
    }

    public get rightOperand(): Operator | undefined { 
        return this._rightOperand;
    }
    public set rightOperand(value: Operator | undefined) {
        this._rightOperand = value;
    } 
}

export class AndOperator extends LogicOperator {
    @serializable readonly _type = OPERATOR_TYPE_AND;
    public get name(): string { return 'And'; }
    public get isConfigured(): boolean { 
        return (this.leftOperand?.isConfigured ?? false)
            && (this.rightOperand?.isConfigured ?? false) 
    }
    
    public getResult(): boolean {
        if (!this.isConfigured) { throw `${this.name} Operator is not configured!` }

        return this.leftOperand!.getResult() 
            && this.rightOperand!.getResult();
    }
}

export class OrOperator extends LogicOperator {
    @serializable readonly _type = OPERATOR_TYPE_OR;
    public get name(): string { return 'Or'; }
    public get isConfigured(): boolean { 
        return (this.leftOperand?.isConfigured ?? false)
            && (this.rightOperand?.isConfigured ?? false) 
    }
    
    public getResult(): boolean {
        if (!this.isConfigured) { throw `${this.name} Operator is not configured!` }

        return this.leftOperand!.getResult() 
            || this.rightOperand!.getResult();
    }
}

// ** LOGIC - SINGLE/UNARY
export abstract class UnaryLogicOperator extends Operator {
    private _oper?: Operator;

    public get operator(): Operator | undefined { 
        return this._oper; 
    }
    public set operator(oper: Operator | undefined) { 
        this._oper = oper; 
    }
}

export class NotOperator extends UnaryLogicOperator {
    @serializable readonly _type = OPERATOR_TYPE_NOT;
    public get name(): string { return 'Not'; }
    public get isConfigured(): boolean {
        return this.operator?.isConfigured ?? false;
    }

    public getResult(): boolean {
        return !this.operator?.getResult();
    }
}

// CONTROL-BASED
export abstract class ControlBasedOperator extends Operator {
    private _controlId?: string;

    public get isConfigured(): boolean {
        return this._controlId != null && this.isControlBasedDerivedConfigured;
    }

    protected abstract get isControlBasedDerivedConfigured(): boolean;

    public get controlId(): string | undefined { return this._controlId; }
    public set controlId(value: string | undefined) {
        this._controlId = value;
    }
}

// ** CONTROL-BASED - CHECKED
export class IsCheckedOperator extends ControlBasedOperator {
    @serializable readonly _type = OPERATOR_TYPE_ISCHECKED;
    public get name() { return 'Is checked'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

export class IsNotCheckedOperator extends ControlBasedOperator {
    @serializable readonly _type = OPERATOR_TYPE_ISNOTCHECKED;
    public get name() { return 'Is not checked'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

// ** CONTROL-BASED - EQUALITY
export abstract class EqualityOperator extends ControlBasedOperator {}

export class EqualsOperator extends EqualityOperator {
    @serializable readonly _type = OPERATOR_TYPE_EQUALS;
    public get name() { return 'Equals'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

export class NotEqualsOperator extends EqualityOperator {
    @serializable readonly _type = OPERATOR_TYPE_NOTEQUALS;
    public get name() { return 'Not equals'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

export class LikeOperator extends EqualityOperator {
    @serializable readonly _type = OPERATOR_TYPE_LIKE;
    public get name() { return 'Like'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

// ** CONTROL-BASED - SETS
export abstract class SetOperator extends ControlBasedOperator { }

export class InSetOperator extends SetOperator {
    @serializable readonly _type = OPERATOR_TYPE_INSET;
    public get name() { return 'In set'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

export class NotInSetOperator extends SetOperator {
    @serializable readonly _type = OPERATOR_TYPE_NOTINSET;
    public get name() { return 'Not in set'; }

    protected get isControlBasedDerivedConfigured(): boolean {
        return true;
    }

    public getResult(): boolean {
        return false; // TODO: IMPLEMENT
    }
}

const OperatorTypesMap = new Map<string, Clazz<Operator>>();
OperatorTypesMap.set(OPERATOR_TYPE_ALWAYS, AlwaysOperator);
OperatorTypesMap.set(OPERATOR_TYPE_NEVER, NeverOperator);
OperatorTypesMap.set(OPERATOR_TYPE_USERISINROLE, UserIsInRoleOperator);
OperatorTypesMap.set(OPERATOR_TYPE_USERISNOTINROLE, UserIsNotInRoleOperator);
OperatorTypesMap.set(OPERATOR_TYPE_AND, AndOperator);
OperatorTypesMap.set(OPERATOR_TYPE_OR, OrOperator);
OperatorTypesMap.set(OPERATOR_TYPE_NOT, NotOperator);
OperatorTypesMap.set(OPERATOR_TYPE_ISCHECKED, IsCheckedOperator);
OperatorTypesMap.set(OPERATOR_TYPE_ISNOTCHECKED, IsNotCheckedOperator);
OperatorTypesMap.set(OPERATOR_TYPE_EQUALS, EqualsOperator);
OperatorTypesMap.set(OPERATOR_TYPE_NOTEQUALS, NotEqualsOperator);
OperatorTypesMap.set(OPERATOR_TYPE_LIKE, LikeOperator);
OperatorTypesMap.set(OPERATOR_TYPE_INSET, InSetOperator);
OperatorTypesMap.set(OPERATOR_TYPE_NOTINSET, NotInSetOperator);

export function getSerializableListOfOperators(): PropSchemaType {
    return {
        serializer: (value: Operator) => {
            if (value === null || typeof value === undefined) {
                return value;
            }

            const modelSchema =
                getDefaultModelSchema<Operator>(
                    getOperatorClass(value._type)
                );
            return serialize(modelSchema!, value);
        },
        deserializer: (value: any, done: Function, context: any) => {
            const type = 
                getOperatorClass(value._type);
            raw().deserializer(
                value,
                (error: Error, result: any) => {
                    const oper = result as Operator;
                    const operInst = new type(oper._type, oper);
                    done(error, operInst);
                },
                context,
                undefined
            );
        }
    }
}

export function getOperatorClass(operatorType: string): Clazz<Operator> {
    const type = OperatorTypesMap.get(operatorType);
    if (!type) { throw `Unknown type ${operatorType}` };
    return type;
}

export function getDefaultOperator(): Operator {
    return new AlwaysOperator();
}