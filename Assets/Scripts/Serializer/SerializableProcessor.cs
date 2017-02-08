using System;
using System.Reflection;
using FullSerializer;

public class SerializableProcessor : fsObjectProcessor {
	public override void OnBeforeSerialize(Type storageType, object instance) {
		MethodInfo method = storageType.GetMethod("OnBeforeSerialize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		if (method != null) {
			method.Invoke(instance, null);
		}
	}
	public override void OnAfterDeserialize(Type storageType, object instance) {
		MethodInfo method = storageType.GetMethod("OnAfterDeserialize", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
		if (method != null) {
			method.Invoke(instance, null);
		}
	}
}