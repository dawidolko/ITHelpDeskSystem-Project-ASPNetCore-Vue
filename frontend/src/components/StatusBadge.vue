<script setup lang="ts">
import { TicketPriority, TicketStatus } from "../types/ticket.types";

interface Props {
  type: "status" | "priority";
  value: string;
  small?: boolean;
}

const props = withDefaults(defineProps<Props>(), {
  small: false,
});

const getStatusColor = (status: string): string => {
  switch (status) {
    case TicketStatus.New:
      return "bg-blue-500";
    case TicketStatus.Open:
      return "bg-cyan-500";
    case TicketStatus.InProgress:
      return "bg-yellow-500";
    case TicketStatus.OnHold:
      return "bg-gray-500";
    case TicketStatus.Resolved:
      return "bg-green-500";
    case TicketStatus.Closed:
      return "bg-gray-700";
    case TicketStatus.Reopened:
      return "bg-orange-500";
    default:
      return "bg-gray-500";
  }
};

const getPriorityColor = (priority: string): string => {
  switch (priority) {
    case TicketPriority.Critical:
      return "bg-red-600";
    case TicketPriority.High:
      return "bg-orange-500";
    case TicketPriority.Medium:
      return "bg-yellow-500";
    case TicketPriority.Low:
      return "bg-green-500";
    default:
      return "bg-gray-500";
  }
};

const getColor = (): string => {
  return props.type === "status"
    ? getStatusColor(props.value)
    : getPriorityColor(props.value);
};

const formatLabel = (value: string): string => {
  return value.replace(/([A-Z])/g, " $1").trim();
};
</script>

<template>
  <span
    :class="[
      getColor(),
      small ? 'text-xs px-2 py-0.5' : 'text-sm px-3 py-1',
      'inline-block rounded-full text-white font-semibold uppercase tracking-wide',
    ]">
    {{ formatLabel(value) }}
  </span>
</template>
