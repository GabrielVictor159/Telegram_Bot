﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.BOT.Application.Boundaries.Marc;

public class SaveMarcOutput
{
    public required Domain.Products.Marc Marc { get; init; }
}