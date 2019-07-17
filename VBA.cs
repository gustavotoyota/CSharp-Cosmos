using System;

public static class VBA {
	public static dynamic CreateObject(string className) {
		Type classType = Type.GetTypeFromProgID(className);
		
		return Activator.CreateInstance(classType);
	}
}