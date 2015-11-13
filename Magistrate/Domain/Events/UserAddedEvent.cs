﻿using System;

namespace Magistrate.Domain.Events
{
	public class UserAddedEvent : UserLoggedEvent
	{
		public Guid UserID { get; set; }
	}
}